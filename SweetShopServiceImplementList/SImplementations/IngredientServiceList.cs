using System;
using System.Collections.Generic;
using System.Linq;
using SweetShopModel;
using SweetShopServiceDAL.SBindingModel;
using SweetShopServiceDAL.SInterfaces;
using SweetShopServiceDAL.SViewModel;


namespace SweetShopServiceImplementList.SImplementations
{
    public class IngredientServiceList : IIngredientService
    {
        private SDataListSingleton source;
        public IngredientServiceList()
        {
            source = SDataListSingleton.GetInstance();
        }
        public List<IngredientViewModel> GetList()
        {
            List<IngredientViewModel> result = source.Ingredients.Select(rec => new IngredientViewModel
            {
                SId = rec.SId,
                IngredientName = rec.IngredientName
            })
            .ToList();
            return result;
        }
        public IngredientViewModel GetElement(int id)
        {
            Ingredient element = source.Ingredients.FirstOrDefault(rec => rec.SId == id);
            if (element != null)
            {
                return new IngredientViewModel
                {
                    SId = element.SId,
                    IngredientName = element.IngredientName
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(IngredientBindingModel model)
        {
            Ingredient element = source.Ingredients.FirstOrDefault(rec => rec.IngredientName == model.IngredientName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            int maxId = source.Ingredients.Count > 0 ? source.Ingredients.Max(rec => rec.SId) : 0;
            source.Ingredients.Add(new Ingredient
            {
                SId = maxId + 1,
                IngredientName = model.IngredientName
            });
        }
        public void UpdElement(IngredientBindingModel model)
        {
            Ingredient element = source.Ingredients.FirstOrDefault(rec => rec.IngredientName == model.IngredientName && rec.SId != model.SId);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = source.Ingredients.FirstOrDefault(rec => rec.SId == model.SId);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.IngredientName = model.IngredientName;
        }
        public void DelElement(int id)
        {
            Ingredient element = source.Ingredients.FirstOrDefault(rec => rec.SId == id);
            if (element != null)
            {
                source.Ingredients.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
