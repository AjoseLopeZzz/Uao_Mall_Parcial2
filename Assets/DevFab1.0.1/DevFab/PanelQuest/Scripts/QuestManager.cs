using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Singleton asegura que solo exista una �nica instancia de la clase QuestManager durante toda la vida del juego.
public class QuestManager : Singleton<QuestManager>
{
    [Header("Quests")]
    [SerializeField] private Quest[] _questsDisponibles;

    [Header("Inspector Quests")]
    [SerializeField] private InspectorQuestDescripcion _inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;
    
    [Header("Personaje Quests")]
    [SerializeField] private PersonajeQuestDescripcion _personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestContenedor;
    
    [Header("Panel Quests Completado")]
    [SerializeField] private GameObject panelQuestCompletado;
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questRecompensaOro;
    [SerializeField] private TextMeshProUGUI questRecompensaExp;
    [SerializeField] private TextMeshProUGUI questRecompensaItemCantidad;
    //[SerializeField] private Image questRecompensaIcono;

    [Header("Configuraciones Debug")]
    public float x;
    public float y;
    public float width;
    public float height;
    public float x2;
    public float y2;
    [SerializeField] private GameObject _panelInspectorQuests;
    [SerializeField] private GameObject _panelPersonajeQuests;


    public Quest QuestPorReclamar { get; private set; }

    private void Start()
    {
        CargarQuestEnInspector();
    }
    
    //si se ha presionado la tecla "1, 2, 3". Si es as�, llama al m�todo A�adirProgreso con argumentos espec�ficos (un ID de misi�n y una cantidad).
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            A�adirProgreso("1", 1);

        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            A�adirProgreso("2", 1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {    
            A�adirProgreso("3", 1);
        }
    }

    //va a cargar todas las misiones disponibles
    private void CargarQuestEnInspector()
    {
        for (int i = 0; i < _questsDisponibles.Length; i++)
        {
            InspectorQuestDescripcion nuevoQuest= Instantiate(_inspectorQuestPrefab, inspectorQuestContenedor);
            nuevoQuest.ConfigurarQuestUI(_questsDisponibles[i]);
        }
    }
    private void A�adirQuestPorCompletar(Quest questPorCompletar)
    {
        PersonajeQuestDescripcion nuevoQuest = Instantiate(_personajeQuestPrefab, personajeQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }
    public void A�adirQuest(Quest questPorCompletar)
    {
        A�adirQuestPorCompletar(questPorCompletar);
    }
    public void A�adirProgreso(string questID, int cantidad)
    {
        Quest questPorActualizar = QuestExiste(questID);
        questPorActualizar.A�adirProgreso(cantidad);
    }
    private Quest QuestExiste(string questID)
    {
        for (int i = 0; i < _questsDisponibles.Length; i++)
        {
            if (_questsDisponibles[i]._ID == questID)
            {
                return _questsDisponibles[i];
            }
        }
        return null;
    }
    private void MostrarQuestCompletado(Quest questCompletado)
    {
        panelQuestCompletado.SetActive(true);
        questNombre.text = questCompletado._nombre;
        questRecompensaOro.text = questCompletado._recompensaOro.ToString();
        questRecompensaExp.text = questCompletado._recompensaExp.ToString();
        questRecompensaItemCantidad.text = questCompletado._recompensaItem._cantidad.ToString();
        
    }
    private void QuestCompletadoRespuesta(Quest questCompletado)
    {
        QuestPorReclamar = QuestExiste(questCompletado._ID);
        if (QuestPorReclamar != null)
        {
            MostrarQuestCompletado(QuestPorReclamar);
        }
    }
    private void OnEnable()
    {
        Quest.EventoQuestCompletado += QuestCompletadoRespuesta;
    }
    private void OnDisable()
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuesta;
    }

    #region debug paneles
    public void AbrirCerrarPanelInspectorQuests()
    {
        _panelInspectorQuests.SetActive(!_panelInspectorQuests.activeSelf);
    }
    public void AbrirCerrarPanelPersonajeQuests()
    {
        _panelPersonajeQuests.SetActive(!_panelPersonajeQuests.activeSelf);
    }
    private void OnGUI()
    {
        // Define la posici�n y el tama�o del bot�n
        Rect buttonRect = new Rect(x, y, width, height);
        Rect buttonRect2 = new Rect(x2, y2, width, height);


        // Crea un bot�n en la posici�n y tama�o especificados
        if (GUI.Button(buttonRect, "Panel Quest"))
        {
            // Acciones que se realizar�n cuando el bot�n sea presionado
            AbrirCerrarPanelInspectorQuests();
        }
        if (GUI.Button(buttonRect2, "Panel Quest Personaje"))
        {
            // Acciones que se realizar�n cuando el bot�n sea presionado
            AbrirCerrarPanelPersonajeQuests();
        }
    }
    #endregion
}
