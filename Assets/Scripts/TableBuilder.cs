using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public static class TableBuilder
{
    //Paleta de colores
    private static readonly Color COLOR_HEADER_BG = new Color(0.13f, 0.13f, 0.20f);
    private static readonly Color COLOR_HEADER_TEXT = new Color(0.95f, 0.85f, 0.30f);
    private static readonly Color COLOR_ROW_PAR = new Color(0.18f, 0.18f, 0.26f);
    private static readonly Color COLOR_ROW_IMPAR = new Color(0.14f, 0.14f, 0.21f);
    private static readonly Color COLOR_ROW_TEXT = new Color(0.90f, 0.90f, 0.90f);
    private static readonly Color COLOR_BORDE = new Color(0.30f, 0.30f, 0.45f);
    //Método principal para construir la tabla a partir de columnas y filas
    public static VisualElement Construir(string[] columnas, List<string[]> filas)
    {
        // Contenedor raíz de la tabla
        var tabla = new VisualElement();
        tabla.style.flexDirection = FlexDirection.Column;
        tabla.style.flexGrow = 1;
        // Cabecera
        var cabecera = CrearFila(columnas, true);
        tabla.Add(cabecera);
        //Filas de datos
        for (int i = 0; i < filas.Count; i++)
        {
            var fila = CrearFila(filas[i], false, i % 2 == 0);
            tabla.Add(fila);
        }
        return tabla;
    }
    // Helpers privados 
    private static VisualElement CrearFila(string[] celdas, bool esCabecera, bool esPar = true)
    {
        var fila = new VisualElement();
        fila.style.flexDirection = FlexDirection.Row;
        fila.style.borderBottomWidth = 1;
        fila.style.borderBottomColor = COLOR_BORDE;
        fila.style.backgroundColor = esCabecera
            ? COLOR_HEADER_BG
            : (esPar ? COLOR_ROW_PAR : COLOR_ROW_IMPAR);
        // Crear celdas
        foreach (var texto in celdas)
        {
            var celda = new Label(texto);
            celda.style.flexGrow = 1;
            celda.style.flexBasis = 0;
            celda.style.paddingTop = 8;
            celda.style.paddingBottom = 8;
            celda.style.paddingLeft = 10;
            celda.style.paddingRight = 10;
            celda.style.unityTextAlign = TextAnchor.MiddleLeft;
            celda.style.fontSize = esCabecera ? 13 : 12;
            celda.style.color = esCabecera ? COLOR_HEADER_TEXT : COLOR_ROW_TEXT;
            if (esCabecera)
                celda.style.unityFontStyleAndWeight = FontStyle.Bold;
            // Borde derecho entre columnas excepto la última
            celda.style.borderRightWidth = 1;
            celda.style.borderRightColor = COLOR_BORDE;

            fila.Add(celda);
        }
        return fila;
    }
}