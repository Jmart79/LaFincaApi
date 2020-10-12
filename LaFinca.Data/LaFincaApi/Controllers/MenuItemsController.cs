using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaFincaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaFincaApi.Controllers
{
    public class MenuItemsController : ControllerBase
    {
        private readonly MenuItemService _itemService;

        public MenuItemsController(MenuItemService service)
        {
            _itemService = service;
        }

        [HttpGet]
        public ActionResult<List<MenuItem>> ViewAll()
        {
            return _itemService.Get();
        }

        public ActionResult<List<MenuItem>> ViewByCategory(string category)
        {
            return _itemService.GetByCategory(category);
        }

        public ActionResult<List<MenuItem>> ViewByCost(string id)
        {
            double cost = Convert.ToDouble(id);
            return _itemService.GetByPrice(cost);
        }

        public ActionResult<List<MenuItem>> ViewByRange([FromQuery(Name ="lowerBound")] string lowerBound, [FromQuery(Name ="upperBound")]string upperBound)
        {
            double lower = Convert.ToDouble(lowerBound);
            double upper = Convert.ToDouble(upperBound);

            return _itemService.GetByRange(lower, upper);
        }

        public ActionResult<List<MenuItem>> Create(MenuItem item)
        {
            if (!_itemService.DoesItemExist(item.ItemName))
            {
                _itemService.Create(item);
            }
            return ViewAll();
        }

        public IActionResult Update( MenuItem item)
        {
            MenuItem foundItem = _itemService.GetByName(item.ItemName);
            if (foundItem == null)
            {
                return NotFound();
            }
            UpdateItem(foundItem, item);
            _itemService.Update(item.ItemName, foundItem);
            return NoContent();
        }

        public IActionResult Delete(string id)
        {
            if (_itemService.DoesItemExist(id))
            {
                _itemService.Remove(itemName: id);
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult FavorItem([FromQuery(Name ="username")] string username, [FromQuery(Name ="itemName")] string itemName)
        {

            return NotFound();
        }

        private void UpdateItem(MenuItem exisitingItem, MenuItem updatedItem)
        {
            if (exisitingItem.Category != updatedItem.Category) exisitingItem.Category = updatedItem.Category;
            if (exisitingItem.Cost != updatedItem.Cost) exisitingItem.Cost = updatedItem.Cost;
            if (exisitingItem.Description != updatedItem.Description) exisitingItem.Description = updatedItem.Description;
            if (exisitingItem.IsAvailable != updatedItem.IsAvailable) exisitingItem.IsAvailable = updatedItem.IsAvailable;
            if (exisitingItem.IsHouseFavorite != updatedItem.IsHouseFavorite) exisitingItem.IsHouseFavorite = updatedItem.IsHouseFavorite;
            if (exisitingItem.ItemName != updatedItem.ItemName) exisitingItem.ItemName = updatedItem.ItemName;
        }


        

    }
}
