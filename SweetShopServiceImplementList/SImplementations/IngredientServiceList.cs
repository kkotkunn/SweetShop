using System;
using System.Collections.Generic;
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
            List<IngredientViewModel> result = new List<IngredientViewModel>();
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                result.Add(new IngredientViewModel
                {
                    SId = source.Ingredients[i].SId,
                    IngredientName = source.Ingredients[i].IngredientName
                });
            }
            return result;
        }
        public IngredientViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].SId == id)
                {
                    return new IngredientViewModel
                    {
                        SId = source.Ingredients[i].SId,
                        IngredientName = source.Ingredients[i].IngredientName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(IngredientBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].SId > maxId)
                {
                    maxId = source.Ingredients[i].SId;
                }
                if (source.Ingredients[i].IngredientName == model.IngredientName)
                {
                    throw new Exception("Уже есть ингредиент с таким названием");
                }
            }
            source.Ingredients.Add(new Ingredient
            {
                SId = maxId + 1,
                IngredientName = model.IngredientName
            });
        }
        public void UpdElement(IngredientBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].SId == model.SId)
                {
                    index = i;
                }
                if (source.Ingredients[i].IngredientName == model.IngredientName &&
                source.Ingredients[i].SId != model.SId)
                {
                    throw new Exception("Уже есть ингредиент с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Ingredients[index].IngredientName = model.IngredientName;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].SId == id)
                {
                    source.Ingredients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}