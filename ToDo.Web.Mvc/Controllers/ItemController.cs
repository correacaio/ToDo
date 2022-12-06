using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Entities;
using ToDo.Domain.Interface;
using ToDo.Web.Mvc.Models;

namespace ToDo.Web.Mvc.Controllers
{
    public class ItemController : Controller
    {
        protected IItemRepository repository;

        public ItemController(IItemRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var items = await repository.GetAllAsync();

            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Description")] CreateItemModel createItemModel)
        {
            if (ModelState.IsValid)
            {
                var item = new Item(createItemModel.Description);
                await repository.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }  

            return View(createItemModel);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await repository.GetAsync(id.Value);
            
            var model = new ItemModel();
            model.Id = item.Id;
            model.Description = item.Description;
            model.Done = item.Done;
            model.CreatedAt = item.CreatedAt;
            
            return View(model);
        }
        
        public async Task<IActionResult> MarkAsDone(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await repository.GetAsync(id.Value);
            
            var model = new ItemModel();
            model.Id = item.Id;
            model.Description = item.Description;
            model.Done = item.Done;
            model.CreatedAt = item.CreatedAt;
            
            return View(model);
        }
        
        public async Task<IActionResult> MarkAsUndone(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await repository.GetAsync(id.Value);
            
            var model = new ItemModel();
            model.Id = item.Id;
            model.Description = item.Description;
            model.Done = item.Done;
            model.CreatedAt = item.CreatedAt;
            
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete([Bind("Id")] ItemModel itemModel)
        {
            await repository.DeleteAsync(itemModel.Id);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public async Task<IActionResult> MarkAsDone([Bind("Id")] ItemModel itemModel)
        {
            var item = await repository.GetAsync(itemModel.Id);
            item.MarkAsDone();
            await repository.EditAsync(item);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public async Task<IActionResult> MarkAsUndone([Bind("Id")] ItemModel itemModel)
        {
            var item = await repository.GetAsync(itemModel.Id);
            item.MarkAsUndone();
            await repository.EditAsync(item);
            return RedirectToAction(nameof(Index));
        }
    }
}
