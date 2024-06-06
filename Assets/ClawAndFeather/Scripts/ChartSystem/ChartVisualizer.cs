using System.Linq;
using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Global/Chart Display")]
[RequireComponent(typeof(PrefabProgressor))]
public class ChartVisualizer : MonoBehaviour
{
    private enum ShowGizmo { Always, [InspectorName("When Selected")] Selected, }
    #region Inspector
    [SerializeField] TextAsset _chart;
    [SerializeField] Vector2 _position;
    [SerializeField] ShowGizmo _showChart;
    [Space]
    [SerializeField] bool _showBPM = true;
    [SerializeField] Color _BPMColor = Color.red;
    [SerializeField, Min(0)] float _BPMLineLength = 6.0f;
    [SerializeField, Min(1)] float _addedLength = 1.0f;
    [Space]
    [SerializeField] bool _showNotes = true;
    [SerializeField] Color _notesColor = Color.yellow;
    [SerializeField] Color _restNotesColor = Color.cyan;
    [SerializeField, Min(0)] float _notesLineLength = 5.0f;
    [Space]
    [Tooltip("Constantly updates the current chart. Useful when making the chart, not recommended otherwise.")]
    [SerializeField] bool _keepChartUpdated = false;
    #endregion

    private SongChart _songChart;
    private PrefabProgressor _progressor;

    private void Awake()
    {
        ParseChart(logSuccess: false);
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
        if (_keepChartUpdated)
        {
            ParseChart(logSuccess: false);
        }

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

            for (int c = 0; c <= totalBeats * _addedLength; c++)
            {
                y = _position.y + transform.position.y + (_progressor.scrollSpeed * c * bps);
                Gizmos.DrawCube(new(_position.x, y), new(_BPMLineLength, 0.05f));
            }
        }
        // Notes
        if (_showNotes)
        {
            foreach (var note in _songChart.Notes)
            {
                Gizmos.color = note.IsRest ? _restNotesColor : _notesColor;
                y = _position.y + transform.position.y + _progressor.scrollSpeed * note.NoteTime;
                Gizmos.DrawWireCube(new(_position.x, y), new(_notesLineLength, 0));
            }
        }
    }

    [ContextMenu("Update Chart")]
    private void ParseChart() => ParseChart(true);
    private void ParseChart(bool logSuccess)
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

            _progressor = _progressor != null ? _progressor : GetComponent<PrefabProgressor>();

            if (logSuccess)
            {
                Debug.Log("Chart was successfully updated!");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Could not update chart {ex.Message}");
        }
    }
}
