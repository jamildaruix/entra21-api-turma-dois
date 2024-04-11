using Microsoft.AspNetCore.Mvc;
using minha_primeira_api.Dtos;
using minha_primeira_api.Models;
using System.Net;

namespace minha_primeira_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContextDB _todoContextDB;

        public TodoItemsController(TodoContextDB todoContextDB)
        {
            this._todoContextDB = todoContextDB;
        }

        [HttpPost]
        public ActionResult<int> PostTodoItem(TodoItemsDto todoDto)
        {
            TodoItemModel model = new TodoItemModel();
            model.Nome = todoDto.Nome;
            model.Apelido = "teste";
            model.Ativo = todoDto.Completo;
            
            _todoContextDB.TodoItemModels.Add(model);
            _todoContextDB.SaveChanges();
            
            return StatusCode(HttpStatusCode.Created.GetHashCode(), model.Id);
        }

        [HttpPut("{id}")]
        public ActionResult<bool> UpdateTodoItem([FromRoute] int id, TodoItemsDto todoDto)
        {
            //BUSCAR NO BANCO POR ID
            
            TodoItemModel model = new TodoItemModel();
            
            model.Id = id;
            model.Nome = todoDto.Nome;
            model.Ativo = todoDto.Completo;
            model.Cadastro = DateTime.Now;

            return StatusCode(HttpStatusCode.OK.GetHashCode(), true);
        }
    }
}
