using System;

[Serializable]
public class Objeto
{
    public int id;
    public string nombre;
    public string tipo;
    public int precio;
    public string rareza;
}

[Serializable]
public class ObjetosWrapper
{
    public Objeto[] objetos;
}