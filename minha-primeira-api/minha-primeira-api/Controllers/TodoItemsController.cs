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

        /// <summary>
        /// Inserir dados
        /// </summary>
        /// <param name="todoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<TodoItemsResponsePostDto> PostTodoItem(TodoItemsDto todoDto)
        {
            var apelidoSplit = todoDto.Nome.Split(' ');
            var apelido = $"ap-{apelidoSplit[0].ToLower()}";

            var existeApelido = _todoContextDB.TodoItemModels.Where(w => w.Apelido == apelido).FirstOrDefault();

            if (existeApelido is not null)
            {
                return NotFound("Apelido já existem na base de dados");
            }

            TodoItemModel model = new TodoItemModel();
            model.Nome = todoDto.Nome;

            model.Apelido = apelido;
            model.Ativo = todoDto.Ativo;

            _todoContextDB.TodoItemModels.Add(model);
            _todoContextDB.SaveChanges();

            var retorno = new TodoItemsResponsePostDto(model.Id, model.Apelido);

            return StatusCode(HttpStatusCode.Created.GetHashCode(), retorno);
        }

        /// <summary>
        /// Atualizar
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<TodoItemsResponseDto> UpdateTodoItem([FromRoute] int id, TodoItemsDto todoDto)
        {
            var modelTodoItem = _todoContextDB.TodoItemModels.Find(id);

            if (modelTodoItem is null)
            {
                return NotFound("Dado não encontrado.");
            }

            modelTodoItem.Nome = todoDto.Nome;
            modelTodoItem.Ativo = todoDto.Ativo;

            _todoContextDB.TodoItemModels.Update(modelTodoItem);
            _todoContextDB.SaveChanges();

            var retorno = new TodoItemsResponseDto(modelTodoItem.Id, modelTodoItem.Nome, modelTodoItem.Ativo, modelTodoItem.Apelido!);

            return StatusCode(HttpStatusCode.OK.GetHashCode(), retorno);
        }

        /// <summary>
        /// Deletar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteTodoItem([FromRoute] int id)
        {
            var modelTodoItem = _todoContextDB.TodoItemModels.Find(id);

            if (modelTodoItem is null)
            {
                return NotFound("Dado não encontrado.");
            }

            _todoContextDB.TodoItemModels.Remove(modelTodoItem);
            _todoContextDB.SaveChanges();

            return Ok(true);
        }

        /// <summary>
        /// Selecinar todos os registros
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<TodoItemsResponseDto>> Get()
        {
            var modelTodoItems = _todoContextDB.TodoItemModels;

            if (modelTodoItems is null)
            {
                return NotFound("Dados não encontrados.");
            }

            List<TodoItemsResponseDto> lista = new List<TodoItemsResponseDto>();

            foreach (var model in modelTodoItems)
            {
                var retorno = new TodoItemsResponseDto(model.Id, model.Nome, model.Ativo, model.Apelido!);
                lista.Add(retorno);
            }

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItemsResponseDto> Get([FromRoute] int id)
        {
            var modelTodoItem = _todoContextDB.TodoItemModels.Find(id);

            if (modelTodoItem is null)
            {
                return NotFound("Dado não encontrado.");
            }

            var retorno = new TodoItemsResponseDto(modelTodoItem.Id, modelTodoItem.Nome, modelTodoItem.Ativo, modelTodoItem.Apelido!);

            return Ok(retorno);
        }
    }
}
