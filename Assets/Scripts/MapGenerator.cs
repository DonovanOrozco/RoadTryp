using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    [Header("Configuración general")]
    public Transform jugador;
    public int segmentosIniciales = 3;
    public float longitudSegmento = 4f;

    [Header("Segmentos por ambiente")]
    public GameObject[] segmentosCiudad;
    public GameObject[] segmentosDesierto;
    public GameObject[] segmentosBosque;

    [Header("Distancia para cambio de ambiente")]
    public float distanciaParaDesierto = 300f;
    public float distanciaParaBosque = 600f;

    [Header("Items")]
    public GameObject[] itemsPrefabs;
    public int cantidadItemsPorSegmento = 3;

    [Header("Obstáculos")]
    public GameObject[] obstaculosPrefabs;
    public int cantidadObstaculosPorSegmento = 2;

    // Lista de segmentos activos en escena, ordenados de atrás (0) a adelante (último)
    private List<GameObject> segmentosActivos = new List<GameObject>();

    // Puntos de spawn para delante y atrás
    private float spawnForwardZ;
    private float spawnBackwardZ;

    void Start()
    {
        // Inicializar puntos de spawn en la posición del jugador
        spawnForwardZ = jugador.position.z;
        spawnBackwardZ = jugador.position.z;

        // Generar segmentos iniciales hacia adelante
        for (int i = 0; i < segmentosIniciales; i++)
            SpawnForwardSegment();
    }

    void Update()
    {
        // Generar nuevo segmento delante si avanzamos suficiente
        if (jugador.position.z - longitudSegmento > (spawnForwardZ - segmentosIniciales * longitudSegmento))
        {
            SpawnForwardSegment();
            RemoveBackwardSegment();
        }

        // Generar nuevo segmento atrás si retrocedemos suficiente
        if (jugador.position.z < spawnBackwardZ)
        {
            SpawnBackwardSegment();
            RemoveForwardSegment();
        }
    }

    // Instancia un segmento adelante
    void SpawnForwardSegment()
    {
        GameObject[] pool = ElegirSegmentosPorDistancia();
        GameObject prefab = pool[Random.Range(0, pool.Length)];

        GameObject seg = Instantiate(prefab, new Vector3(0, 0, spawnForwardZ), Quaternion.identity);
        SpawnItems(seg.transform);
        SpawnObstaculos(seg.transform);

        segmentosActivos.Add(seg);
        spawnForwardZ += longitudSegmento;
    }

    // Instancia un segmento atrás
    void SpawnBackwardSegment()
    {
        GameObject[] pool = ElegirSegmentosPorDistancia();
        GameObject prefab = pool[Random.Range(0, pool.Length)];

        spawnBackwardZ -= longitudSegmento;
        GameObject seg = Instantiate(prefab, new Vector3(0, 0, spawnBackwardZ), Quaternion.identity);
        SpawnItems(seg.transform);
        SpawnObstaculos(seg.transform);

        segmentosActivos.Insert(0, seg);
    }

    // Elimina el segmento más atrás (índice 0)
    void RemoveBackwardSegment()
    {
        if (segmentosActivos.Count == 0) return;
        Destroy(segmentosActivos[0]);
        segmentosActivos.RemoveAt(0);
    }

    // Elimina el segmento más adelante (último índice)
    void RemoveForwardSegment()
    {
        if (segmentosActivos.Count == 0) return;
        int last = segmentosActivos.Count - 1;
        Destroy(segmentosActivos[last]);
        segmentosActivos.RemoveAt(last);
    }

    // Genera ítems dentro del camino
    void SpawnItems(Transform segmento)
    {
        for (int i = 0; i < cantidadItemsPorSegmento; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-3f, 3f),
                0.5f,
                segmento.position.z + Random.Range(-longitudSegmento / 2 + 0.5f, longitudSegmento / 2 - 0.5f)
            );
            Instantiate(
                itemsPrefabs[Random.Range(0, itemsPrefabs.Length)],
                pos,
                Quaternion.identity
            );
        }
    }

    // Genera obstáculos dentro del camino
    void SpawnObstaculos(Transform segmento)
    {
        for (int i = 0; i < cantidadObstaculosPorSegmento; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-3f, 3f),
                0.5f,
                segmento.position.z + Random.Range(-longitudSegmento / 2 + 0.5f, longitudSegmento / 2 - 0.5f)
            );
            Instantiate(
                obstaculosPrefabs[Random.Range(0, obstaculosPrefabs.Length)],
                pos,
                Quaternion.identity
            );
        }
    }

    // Selecciona el set de prefabs según la distancia recorrida
    GameObject[] ElegirSegmentosPorDistancia()
    {
        float d = jugador.position.z;
        if (d < distanciaParaDesierto)
            return segmentosCiudad;
        else if (d < distanciaParaBosque)
            return segmentosDesierto;
        else
            return segmentosBosque;
    }
}
