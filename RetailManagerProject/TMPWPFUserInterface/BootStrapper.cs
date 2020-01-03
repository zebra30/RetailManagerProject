using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TMPWPFUserInterface.Helpers;
using TMPWPFUserInterface.ViewModels;

namespace TMPWPFUserInterface
{
    public class BootStrapper : BootstrapperBase
    {
        //Dependency Injection with SimpleContainer
        private SimpleContainer _container = new SimpleContainer();
        public BootStrapper()
        {
            Initialize();

            ConventionManager.AddElementConvention<PasswordBox>(
                PasswordBoxHelper.BoundPasswordProperty,
                  "Password",
                  "PasswordChanged");
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            //On Start Up, Launch ShellViewModel instead of MainWindow.xaml.
            //ViewModel will launch ShellView.xaml
            DisplayRootViewFor<ShellViewModel>();
        }

        /// <summary>
        /// Container hold an instance of itself
        /// </summary>
        protected override void Configure()
        {
            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>() //Window Manager
                .Singleton<IEventAggregator, EventAggregator>() //Event Messages through out application
                .Singleton<IAPIHelper, APIHelper>();

            GetType().Assembly.GetTypes()   //Get all the types in the Assembly
                .Where(type => type.IsClass) //Where Type is a Class
                .Where(type => type.Name.EndsWith("ViewModel")) // Where the name of the class ends with ViewModel
                .ToList() //
                .ForEach(viewModelType => _container.RegisterPerRequest(
                        viewModelType, viewModelType.ToString(), viewModelType)); //register each ViewModel class
        
        }
        
        /// <summary>
        /// Use to get instance of an object
        /// </summary>
        /// <param name="service"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
