using L01_2020GR603.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020GR603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentario : ControllerBase
    {
        private readonly infocontext _blogDB;

        public comentario(infocontext blogDB)
        {
            _blogDB = blogDB;
        }
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<comentarios> listausuarios = (from e in _blogDB.comentarios
                                                  select e).ToList();
            if (listausuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listausuarios);
        }
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarUsuarios([FromBody] comentarios comentario)
        {
            try
            {
                _blogDB.comentarios.Add(comentario);
                _blogDB.SaveChanges();
                return Ok(comentario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Actualizar un registro median el parametro del ID.
        [HttpPut]
        [Route("actualizar/id")]
        public IActionResult ActualizarRegistro(int id, [FromBody] comentarios modificarCalificaciones)
        {
            //Para actualizar un registro, obtenemos el original desde la base de datos
            //al cual se le alterara una propiedad
            comentarios? calificacionesActuales = (from c in _blogDB.comentarios
                                                      where c.cometarioId == id
                                                      select c).FirstOrDefault();
            //Verificación de existencia del registro
            if (calificacionesActuales == null)
            { return NotFound(); }

            //Si se encuentra el registro, se alteran los campos
            calificacionesActuales.publicacionId = modificarCalificaciones.publicacionId;
            calificacionesActuales.comentario = modificarCalificaciones.comentario;
            calificacionesActuales.usuarioId = modificarCalificaciones.usuarioId;


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

            comentarios delete = (from e in _blogDB.comentarios
                                     where e.cometarioId == id
                                     select e).FirstOrDefault();

            //Verificamos que exista el registro segun su ID

            if (delete == null)

                return NotFound();

            //Ejecutamos la accion de elminar el registro

            _blogDB.comentarios.Attach(delete);
            _blogDB.comentarios.Remove(delete);
            _blogDB.SaveChanges();

            return Ok(delete);

        }
        //Método para mostrar los registro mediante Usuario
        //filtrar por Usuario
        [HttpGet]
        [Route("find/{filtropublicacion}")]
        public IActionResult findbyrol(int filtropublicacion)
        {
            comentarios? publicacion = (from e in _blogDB.comentarios
                                           where e.publicacionId == filtropublicacion
                                           select e).FirstOrDefault();

            if (publicacion == null)
            {
                return NotFound();
            }
            return Ok(publicacion);
        }
    }
}

