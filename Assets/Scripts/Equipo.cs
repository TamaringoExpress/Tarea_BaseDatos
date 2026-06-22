using System;

[Serializable]
public class Equipo
{
    public int id;
    public string nombre;
    public string region;
    public int victorias;
    public int derrotas;
    public int posicion;
}

[Serializable]
public class EquiposWrapper
{
    public Equipo[] equipos;
}