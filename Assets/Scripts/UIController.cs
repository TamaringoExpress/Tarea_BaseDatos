using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public enum TipoJSON
    {
        Tienda,
        Torneo,
        Catalogo
    }
    [Header("Configuración")]
    public TipoJSON jsonActivo = TipoJSON.Tienda;
    void Start()
    {
        // 1. Referencia al ScrollView
        var root = GetComponent<UIDocument>().rootVisualElement;
        var contenedor = root.Q<ScrollView>("contenedor-scroll");

        if (contenedor == null)
        {
            Debug.LogError("No se encontró 'contenedor-scroll' en el UXML.");
            return;
        }

        // 2. Decidir qué JSON cargar según el enum
        switch (jsonActivo)
        {
            case TipoJSON.Tienda: CargarTienda(contenedor); break;
            case TipoJSON.Torneo: CargarTorneo(contenedor); break;
            case TipoJSON.Catalogo: CargarCatalogo(contenedor); break;
        }
    }

    //Ejercicio 1: Tienda
    void CargarTienda(ScrollView contenedor)
    {
        TextAsset archivo = Resources.Load<TextAsset>("tienda");
        if (archivo == null) { Debug.LogError("No se encontró tienda.json"); return; }

        ObjetosWrapper wrapper = JsonUtility.FromJson<ObjetosWrapper>(archivo.text);
        if (wrapper == null || wrapper.objetos == null)
        {
            Debug.LogError("Error al parsear tienda.json");
            return;
        }

        Debug.Log("Objetos cargados: " + wrapper.objetos.Length);

        string[] columnas = { "ID", "Nombre", "Tipo", "Precio", "Rareza" };

        var filas = new List<string[]>();
        foreach (var o in wrapper.objetos)
        {
            filas.Add(new[]
            {
                o.id.ToString(),
                o.nombre,
                o.tipo,
                o.precio.ToString(),
                o.rareza
            });
        }

        contenedor.Clear();
        contenedor.Add(TableBuilder.Construir(columnas, filas));
    }
    // Ejercicio 2: 
    void CargarTorneo(ScrollView contenedor)
    {
        TextAsset archivo = Resources.Load<TextAsset>("torneo");
        if (archivo == null) { Debug.LogError("No se encontró torneo.json"); return; }

        // Usa EquiposWrapper, NO ObjetosWrapper
        EquiposWrapper wrapper = JsonUtility.FromJson<EquiposWrapper>(archivo.text);
        if (wrapper == null || wrapper.equipos == null)
        {
            Debug.LogError("Error al parsear torneo.json");
            return;
        }
        Debug.Log("Equipos cargados: " + wrapper.equipos.Length);
        string[] columnas = { "Posición", "Equipo", "Región", "Victorias", "Derrotas" };
        var filas = new List<string[]>();
        foreach (var e in wrapper.equipos)
        {
            filas.Add(new[]
            {
                e.posicion.ToString(),
                e.nombre,
                e.region,
                e.victorias.ToString(),
                e.derrotas.ToString()
            });
        }
        contenedor.Clear();
        contenedor.Add(TableBuilder.Construir(columnas, filas));
    }
    //Ejercicio 3
    void CargarCatalogo(ScrollView contenedor)
    {
        TextAsset archivo = Resources.Load<TextAsset>("catalogo");
        if (archivo == null) { Debug.LogError("No se encontró catalogo.json"); return; }
        VideojuegosWrapper wrapper = JsonUtility.FromJson<VideojuegosWrapper>(archivo.text);
        if (wrapper == null || wrapper.videojuegos == null)
        {
            Debug.LogError("Error al parsear catalogo.json");
            return;
        }
        Debug.Log("Videojuegos cargados: " + wrapper.videojuegos.Length);
        string[] columnas = { "ID", "Título", "Género", "Año", "Precio ($)", "Calificación", "Desarrollador" };
        var filas = new List<string[]>();
        foreach (var v in wrapper.videojuegos)
        {
            filas.Add(new[]
            {
                v.id.ToString(),
                v.titulo,
                v.genero,
                v.anio.ToString(),           // ← int convertido a string
                v.precio.ToString("F2"),
                v.calificacion.ToString("F1"),
                v.desarrollador
            });
        }
        contenedor.Clear();
        contenedor.Add(TableBuilder.Construir(columnas, filas));
    }
}