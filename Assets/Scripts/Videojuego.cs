using System;

[Serializable]
public class Videojuego
{
    public int id;
    public string titulo;
    public string genero;
    public int anio;
    public float precio;
    public float calificacion;
    public string desarrollador;
}

[Serializable]
public class VideojuegosWrapper
{
    public Videojuego[] videojuegos;
}