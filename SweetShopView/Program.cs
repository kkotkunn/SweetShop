using System;
using System.Windows.Forms;
using SweetShopServiceDAL.SInterfaces;
using SweetShopServiceImplementList.SImplementations;
using Unity;
using Unity.Lifetime;

namespace SweetShopView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormSMain>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<ISStockService, SStockServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISClientService, SClientServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IIngredientService, IngredientServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISweetService, SweetServiceList>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISMainService, SMainServiceList>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
