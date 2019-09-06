using System;
using System.Collections.Generic;
using System.Linq;
using SweetShopModel;
using SweetShopServiceDAL.SBindingModel;
using SweetShopServiceDAL.SInterfaces;
using SweetShopServiceDAL.SViewModel;

namespace SweetShopServiceImplementList.SImplementations
{
    public class SweetServiceList : ISweetService
    {
        private SDataListSingleton source;
        public SweetServiceList()
        {
            source = SDataListSingleton.GetInstance();
        }
        public List<SweetViewModel> GetList()
        {
            List<SweetViewModel> result = source.Sweets
            .Select(rec => new SweetViewModel
            {
                SId = rec.SId,
                SweetName = rec.SweetName,
                SPrice = rec.SPrice,
                SweetIngredients = source.SweetIngredients
            .Where(recPC => recPC.SweetId == rec.SId)
            .Select(recPC => new SweetIngredientViewModel
            {
                SId = recPC.SId,
                SweetId = recPC.SweetId,
                IngredientId = recPC.IngredientId,
                IngredientName = source.Ingredients.FirstOrDefault(recC => recC.SId == recPC.IngredientId)?.IngredientName,
                SCount = recPC.SCount
            })
            .ToList()
            })
            .ToList();
            return result;
        }
        public SweetViewModel GetElement(int id)
        {
            Sweet element = source.Sweets.FirstOrDefault(rec => rec.SId == id);
            if (element != null)
            {
                return new SweetViewModel
                {
                    SId = element.SId,
                    SweetName = element.SweetName,
                    SPrice = element.SPrice,
                    SweetIngredients = source.SweetIngredients
                .Where(recPC => recPC.SweetId == element.SId)
                .Select(recPC => new SweetIngredientViewModel
                {
                    SId = recPC.SId,
                    SweetId = recPC.SweetId,
                    IngredientId = recPC.IngredientId,
                    IngredientName = source.Ingredients.FirstOrDefault(recC => recC.SId == recPC.IngredientId)?.IngredientName,
                    SCount = recPC.SCount
                })
                .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SweetBindingModel model)
        {
            Sweet element = source.Sweets.FirstOrDefault(rec => rec.SweetName == model.SweetName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Sweets.Count > 0 ? source.Sweets.Max(rec => rec.SId) : 0;
            source.Sweets.Add(new Sweet
            {
                SId = maxId + 1,
                SweetName = model.SweetName,
                SPrice = model.SPrice
            });
            int maxPCId = source.SweetIngredients.Count > 0 ? source.SweetIngredients.Max(rec => rec.SId) : 0;
            var groupIngredients = model.SweetIngredients
            .GroupBy(rec => rec.IngredientId)
            .Select(rec => new
            {
                IngredientId = rec.Key,
                SCount = rec.Sum(r => r.SCount)
            });
            foreach (var groupIngredient in groupIngredients)
            {
                source.SweetIngredients.Add(new SweetIngredient
                {
                    SId = ++maxPCId,
                    SweetId = maxId + 1,
                    IngredientId = groupIngredient.IngredientId,
                    SCount = groupIngredient.SCount
                });
            }
        }
        public void UpdElement(SweetBindingModel model)
        {
            Sweet element = source.Sweets.FirstOrDefault(rec => rec.SweetName == model.SweetName && rec.SId != model.SId);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Sweets.FirstOrDefault(rec => rec.SId == model.SId);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SweetName = model.SweetName;
            element.SPrice = model.SPrice;
            int maxPCId = source.SweetIngredients.Count > 0 ? source.SweetIngredients.Max(rec => rec.SId) : 0;
            var ingrIds = model.SweetIngredients.Select(rec => rec.IngredientId).Distinct();
            var updateIngredients = source.SweetIngredients.Where(rec => rec.SweetId == model.SId && ingrIds.Contains(rec.IngredientId));
            foreach (var updateIngredient in updateIngredients)
            {
                updateIngredient.SCount = model.SweetIngredients.FirstOrDefault(rec => rec.SId == updateIngredient.SId).SCount;
            }
            source.SweetIngredients.RemoveAll(rec => rec.SweetId == model.SId && !ingrIds.Contains(rec.IngredientId));
            var groupIngredients = model.SweetIngredients
            .Where(rec => rec.SId == 0)
            .GroupBy(rec => rec.IngredientId)
            .Select(rec => new
            {
                IngredientId = rec.Key,
                SCount = rec.Sum(r => r.SCount)
            });
            foreach (var groupIngredient in groupIngredients)
            {
                SweetIngredient elementPC = source.SweetIngredients.FirstOrDefault(rec => rec.SweetId == model.SId && rec.IngredientId == groupIngredient.IngredientId);
                if (elementPC != null)
                {
                    elementPC.SCount += groupIngredient.SCount;
                }
                else
                {
                    source.SweetIngredients.Add(new SweetIngredient
                    {
                        SId = ++maxPCId,
                        SweetId = model.SId,
                        IngredientId = groupIngredient.IngredientId,
                        SCount = groupIngredient.SCount
                    });
                }
            }
        }
        public void DelElement(int id)
        {
            Sweet element = source.Sweets.FirstOrDefault(rec => rec.SId == id);
            if (element != null)
            {
                source.SweetIngredients.RemoveAll(rec => rec.SweetId == id);
                source.Sweets.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
