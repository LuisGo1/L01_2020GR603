using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020GR603.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;



namespace L01_2020GR603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class blogDBController : ControllerBase
    {
        private readonly infocontext _blogDB;

        public blogDBController(infocontext blogDB)
        {
            _blogDB = blogDB;
        }
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get() 
        {
            List<usuarios> listausuarios = (from e in _blogDB.usuarios
                                            select e).ToList();
            if (listausuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listausuarios);
        }

        //obtener datos por Id de usuario
        
        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            usuarios? usuario = (from e in _blogDB.usuarios
                                 where e.usuarioId == id
                                 select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);

        }

        // filtro por nombre y apellido
        [HttpGet]
        [Route("find/(filtro)")]
        public IActionResult findbynameandlastname(string filtro)
        {
            usuarios? usuario = (from e in _blogDB.usuarios
                                 where e.nombre.Contains(filtro)
                                || e.apellido.Contains(filtro)
                                 select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
        //filtrar por rol
        [HttpGet]
        [Route("find/{filtroRolId}")]
        public IActionResult findbyrol(int filtroRolId)
        {
            usuarios? usuario = (from e in _blogDB.usuarios
                                 where e.rolId == filtroRolId
                                 select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarUsuarios([FromBody] usuarios usuario)
        {
            try
            {
                _blogDB.usuarios.Add(usuario);
                _blogDB.SaveChanges();
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Actualizar un registro median el parametro del ID.
        [HttpPut]
        [Route("actualizar/id")]
        public IActionResult ActualizarRegistro(int id, [FromBody] usuarios modificarCalificaciones)
        {
            //Para actualizar un registro, obtenemos el original desde la base de datos
            //al cual se le alterara una propiedad
            usuarios? calificacionesActuales = (from c in _blogDB.usuarios
                                                      where c.usuarioId == id
                                                      select c).FirstOrDefault();
            //Verificación de existencia del registro
            if (calificacionesActuales == null)
            { return NotFound(); }

            //Si se encuentra el registro, se alteran los campos
            calificacionesActuales.rolId = modificarCalificaciones.rolId;
            calificacionesActuales.nombreUsuario = modificarCalificaciones.nombreUsuario;
            calificacionesActuales.clave = modificarCalificaciones.clave;
            calificacionesActuales.nombre = modificarCalificaciones.nombre;
            calificacionesActuales.apellido = modificarCalificaciones.apellido;


            //Se marca el registro como modificado
            //Luego se envia la modificacion a la base de datos
            _blogDB.Entry(calificacionesActuales).State = EntityState.Modified;
            _blogDB.SaveChanges();

            return Ok();

        }
        [HttpDelete]

        [Route("eliminar/{id}")]

        public IActionResult EliminarEquipo(int id) 
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos //al cual eliminaremos

           usuarios delete = (from e in _blogDB.usuarios
                               where e.usuarioId == id
                               select e).FirstOrDefault();

            //Verificamos que exista el registro segun su ID

            if (delete == null)

                return NotFound();

            //Ejecutamos la accion de elminar el registro
            
            _blogDB.usuarios. Attach(delete);
            _blogDB.usuarios.Remove(delete); 
            _blogDB.SaveChanges();

            return Ok(delete);

        }


    }
}



