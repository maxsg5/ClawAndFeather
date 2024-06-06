using System.Linq;
using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Global/Chart Display")]
[RequireComponent(typeof(PrefabProgressor))]
public class ChartVisualizer : MonoBehaviour
{
    private enum ShowGizmo { Always, [InspectorName("When Selected")] Selected, }
    #region Inspector
    [SerializeField] private TextAsset _chart;
    [SerializeField] private ShowGizmo _showChart;
    [Space]
    [SerializeField] private bool _showBPM = true;
    [SerializeField] private Color _BPMColor = Color.red;
    [SerializeField, Min(0)] private float _BPMLineLength = 6.0f;
    [Space]
    [SerializeField] private bool _showNotes = true;
    [SerializeField] private Color _notesColor = Color.yellow;
    [SerializeField, Min(0)] private float _notesLineLength = 5.0f;
    #endregion

    private SongChart _songChart;
    private PrefabProgressor _progressor;
    private Vector2 _playerPosition;

    private void Awake()
    {
        ParseChart();
    }

    private void OnDrawGizmos()
    {
        if (_showChart == ShowGizmo.Always && _songChart != null)
        {
            DrawLines();
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (_showChart == ShowGizmo.Selected && _songChart != null)
        {
            DrawLines();
        }
    }

    private void DrawLines()
    {
        float y;
        // BPM
        if (_showBPM)
        {
            Gizmos.color = _BPMColor;

            float bps = 60f / _songChart.BPM;
            int totalBeats = Mathf.CeilToInt(_songChart.Length * bps);

            for (int c = 0; c <= totalBeats; c++)
            {
                y = transform.position.y + (_progressor.scrollSpeed * c * bps);
                Gizmos.DrawCube(new(transform.position.x, y), new(_BPMLineLength, 0.05f));
            }
        }
        // Notes
        if (_showNotes)
        {
            Gizmos.color = _notesColor;
            foreach (var note in _songChart.Notes.Where(n => !n.IsRest))
            {
                y = transform.position.y + _progressor.scrollSpeed * note.NoteTime;
                Gizmos.DrawWireCube(new(transform.position.x, y), new(_notesLineLength, 0));
            }
        }
    }

    [ContextMenu("Update Chart")]
    private void ParseChart()
    {
        try
        {
            if (_chart == null)
            {
                throw new System.Exception("as there is no chart to update.");
            }

            try
            {
                SongParser.ReadCSVFile(_chart, out _songChart);
            }
            catch (System.Exception)
            {
                throw new System.Exception("due to a file parsing error.");
            }

            _progressor =
            _progressor != null ? _progressor : GetComponent<PrefabProgressor>();

            _playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

            Debug.Log("Chart was successfully updated!");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Could not update chart {ex.Message}");
        }
    }
}
