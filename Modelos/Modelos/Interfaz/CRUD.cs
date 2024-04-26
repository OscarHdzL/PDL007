using System.Collections.Generic;

namespace Modelos.Interfaz
{
    public interface CRUD<T>
    {
        Response.ResponseGeneric<List<T>> Consultar(T model);
        Response.Response Operacion(T model);
    }
    public enum TipoOperacion
    {
        consultar = 1,
        agregar = 2,
        modificar = 3,
        eliminar = 4
    }
}
