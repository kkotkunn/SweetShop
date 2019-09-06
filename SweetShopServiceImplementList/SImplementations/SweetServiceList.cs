using System;
using System.Collections.Generic;
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
            List<SweetViewModel> result = new List<SweetViewModel>();
            for (int i = 0; i < source.Sweets.Count; ++i)
            {
                // требуется дополнительно получить список ингредиентов для изделия и их количество
                List<SweetIngredientViewModel> sweetIngredients = new List<SweetIngredientViewModel>();
                for (int j = 0; j < source.SweetIngredients.Count; ++j)
                {
                    if (source.SweetIngredients[j].SweetId == source.Sweets[i].SId)
                    {
                        string ingredientName = string.Empty;
                        for (int k = 0; k < source.Ingredients.Count; ++k)
                        {
                            if (source.SweetIngredients[j].IngredientId == source.Ingredients[k].SId)
                            {
                                ingredientName = source.Ingredients[k].IngredientName;
                                break;
                            }
                        }
                        sweetIngredients.Add(new SweetIngredientViewModel
                        {
                            SId = source.SweetIngredients[j].SId,
                            SweetId = source.SweetIngredients[j].SweetId,
                            IngredientId = source.SweetIngredients[j].IngredientId,
                            IngredientName = ingredientName,
                            SCount = source.SweetIngredients[j].SCount
                        });
                    }
                }
                result.Add(new SweetViewModel
                {
                    SId = source.Sweets[i].SId,
                    SweetName = source.Sweets[i].SweetName,
                    SPrice = source.Sweets[i].SPrice,
                    SweetIngredients = sweetIngredients
                });
            }
            return result;
        }
        public SweetViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Sweets.Count; ++i)
            {
                // требуется дополнительно получить список ингредиентов для изделия и их количество
                List<SweetIngredientViewModel> sweetIngredients = new List<SweetIngredientViewModel>();
                for (int j = 0; j < source.SweetIngredients.Count; ++j)
                {
                    if (source.SweetIngredients[j].SweetId == source.Sweets[i].SId)
                    {
                        string ingredientName = string.Empty;
                        for (int k = 0; k < source.Ingredients.Count; ++k)
                        {
                            if (source.SweetIngredients[j].IngredientId == source.Ingredients[k].SId)
                            {
                                ingredientName = source.Ingredients[k].IngredientName;
                                break;
                            }
                        }
                        sweetIngredients.Add(new SweetIngredientViewModel
                        {
                            SId = source.SweetIngredients[j].SId,
                            SweetId = source.SweetIngredients[j].SweetId,
                            IngredientId = source.SweetIngredients[j].IngredientId,
                            IngredientName = ingredientName,
                            SCount = source.SweetIngredients[j].SCount
                        });
                    }
                }
                if (source.Sweets[i].SId == id)
                {
                    return new SweetViewModel
                    {
                        SId = source.Sweets[i].SId,
                        SweetName = source.Sweets[i].SweetName,
                        SPrice = source.Sweets[i].SPrice,
                        SweetIngredients = sweetIngredients
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(SweetBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Sweets.Count; ++i)
            {
                if (source.Sweets[i].SId > maxId)
                {
                    maxId = source.Sweets[i].SId;
                }
                if (source.Sweets[i].SweetName == model.SweetName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Sweets.Add(new Sweet
            {
                SId = maxId + 1,
                SweetName = model.SweetName,
                SPrice = model.SPrice
            });
            // ингредиенты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.SweetIngredients.Count; ++i)
            {
                if (source.SweetIngredients[i].SId > maxPCId)
                {
                    maxPCId = source.SweetIngredients[i].SId;
                }
            }
            // убираем дубли по ингредиентам
            for (int i = 0; i < model.SweetIngredients.Count; ++i)
            {
                for (int j = 1; j < model.SweetIngredients.Count; ++j)
                {
                    if (model.SweetIngredients[i].IngredientId ==
                    model.SweetIngredients[j].IngredientId)
                    {
                        model.SweetIngredients[i].SCount +=
                        model.SweetIngredients[j].SCount;
                        model.SweetIngredients.RemoveAt(j--);
                    }
                }
            }
            // добавляем ингредиенты
            for (int i = 0; i < model.SweetIngredients.Count; ++i)
            {
                source.SweetIngredients.Add(new SweetIngredient
                {
                    SId = ++maxPCId,
                    SweetId = maxId + 1,
                    IngredientId = model.SweetIngredients[i].IngredientId,
                    SCount = model.SweetIngredients[i].SCount
                });
            }
        }
        public void UpdElement(SweetBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Sweets.Count; ++i)
            {
                if (source.Sweets[i].SId == model.SId)
                {
                    index = i;
                }
                if (source.Sweets[i].SweetName == model.SweetName &&
                source.Sweets[i].SId != model.SId)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Sweets[index].SweetName = model.SweetName;
            source.Sweets[index].SPrice = model.SPrice;
            int maxPCId = 0;
            for (int i = 0; i < source.SweetIngredients.Count; ++i)
            {
                if (source.SweetIngredients[i].SId > maxPCId)
                {
                    maxPCId = source.SweetIngredients[i].SId;
                }
            }
            // обновляем существуюущие ингредиенты
            for (int i = 0; i < source.SweetIngredients.Count; ++i)
            {
                if (source.SweetIngredients[i].SweetId == model.SId)
                {
                    bool flag = true;
                    for (int j = 0; j < model.SweetIngredients.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.SweetIngredients[i].SId == model.SweetIngredients[j].SId)
                        {
                            source.SweetIngredients[i].SCount = model.SweetIngredients[j].SCount;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.SweetIngredients.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.SweetIngredients.Count; ++i)
            {
                if (model.SweetIngredients[i].SId == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.SweetIngredients.Count; ++j)
                    {
                        if (source.SweetIngredients[j].SweetId == model.SId &&
                        source.SweetIngredients[j].IngredientId == model.SweetIngredients[i].IngredientId)
                        {
                            source.SweetIngredients[j].SCount += model.SweetIngredients[i].SCount;
                            model.SweetIngredients[i].SId = source.SweetIngredients[j].SId;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.SweetIngredients[i].SId == 0)
                    {
                        source.SweetIngredients.Add(new SweetIngredient
                        {
                            SId = ++maxPCId,
                            SweetId = model.SId,
                            IngredientId = model.SweetIngredients[i].IngredientId,
                            SCount = model.SweetIngredients[i].SCount
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            // удаяем записи по ингредиентам при удалении изделия
            for (int i = 0; i < source.SweetIngredients.Count; ++i)
            {
                if (source.SweetIngredients[i].SweetId == id)
                {
                    source.SweetIngredients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Sweets.Count; ++i)
            {
                if (source.Sweets[i].SId == id)
                {
                    source.Sweets.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}