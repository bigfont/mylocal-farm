// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;
using FarmApi.Models;
using FarmApi.DAL;
using FarmApi.DAL.Interfaces;

namespace FarmApi.Controllers
{
    // api/todo
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET /api/todo
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _unitOfWork.TodoItemRepository.All();
        }

        // GET /api/todo/1
        [HttpGet("{id:int}", Name = "GetByIdRoute")]
        public IActionResult GetById(int id)
        {
            var item = _unitOfWork.TodoItemRepository.GetById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return new ObjectResult(item);
        }

        // POST /api/todo
        [HttpPost]
        public void CreateTodoItem([FromBody] TodoItem item)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
            }
            else
            {
                _unitOfWork.TodoItemRepository.Add(item);
                Context.Response.StatusCode = 201;
            }
        }

        // DELETE /api/todo/1
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            if (_unitOfWork.TodoItemRepository.TryDelete(id))
            {
                return new HttpStatusCodeResult(204);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}
