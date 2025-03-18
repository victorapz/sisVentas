using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;
namespace CapaNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria objcd_categoria = new CD_Categoria();

        public List<Categoria> Listar() {
            return objcd_categoria.Listar();
        }

        public int Registrar(Categoria obj, out string mensaje) {
            mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                mensaje += "No se pudo registrar la categoría, ingrese una descripción. ";
            }
            if (mensaje != "")
            {    
                return 0;
            }
            else
            {
                return objcd_categoria.Registrar(obj, out mensaje);
            }    
        }
        public bool Editar(Categoria obj, out string mensaje) {
            mensaje = string.Empty;

            if (obj.Descripcion == "") 
            {
                mensaje += "No se puedo editar la categoría, ingrese una descripción";
            }
            if (mensaje != "")
            {
                return false;
            }
            else {
                return objcd_categoria.Editar(obj, out mensaje);
            }
        }

        public bool Eliminar(Categoria obj, out string mensaje) {
            return objcd_categoria.Eliminar(obj, out mensaje);
        }

    }
}
