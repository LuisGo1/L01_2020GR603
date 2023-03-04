using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020GR603.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace L01_2020GR603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        private readonly infocontext _blogDB;

        public CalificacionesController(infocontext blogDB)
        {
            _blogDB = blogDB;
        }
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<calificaciones> listausuarios = (from e in _blogDB.calificaciones
                                            select e).ToList();
            if (listausuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listausuarios);
        }
        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarUsuarios([FromBody] calificaciones calificaciones)
        {
            try
            {
                _blogDB.calificaciones.Add(calificaciones);
                _blogDB.SaveChanges();
                return Ok(calificaciones);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Actualizar un registro median el parametro del ID.
        [HttpPut]
        [Route("actualizar/id")]
        public IActionResult ActualizarRegistro(int id, [FromBody] calificaciones modificarCalificaciones)
        {
            //Para actualizar un registro, obtenemos el original desde la base de datos
            //al cual se le alterara una propiedad
            calificaciones? calificacionesActuales = (from c in _blogDB.calificaciones
                                                where c.calificacionId == id
                                                select c).FirstOrDefault();
            //Verificación de existencia del registro
            if (calificacionesActuales == null)
            { return NotFound(); }

            //Si se encuentra el registro, se alteran los campos
            calificacionesActuales.publicacionId = modificarCalificaciones.publicacionId;
            calificacionesActuales.usuarioId = modificarCalificaciones.usuarioId;
            calificacionesActuales.calificacion = modificarCalificaciones.calificacion;
    

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

            calificaciones delete = (from e in _blogDB.calificaciones
                               where e.calificacionId == id
                               select e).FirstOrDefault();

            //Verificamos que exista el registro segun su ID

            if (delete == null)

                return NotFound();

            //Ejecutamos la accion de elminar el registro

            _blogDB.calificaciones.Attach(delete);
            _blogDB.calificaciones.Remove(delete);
            _blogDB.SaveChanges();

            return Ok(delete);

        }
                                                                                                                                                                                          

     

    }
}
