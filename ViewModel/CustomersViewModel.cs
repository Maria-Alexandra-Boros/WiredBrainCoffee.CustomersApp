using System.Collections.ObjectModel;

using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.Model;
using WiredBrainCoffee.CustomersApp.ViewModel;

namespace WiredBrainCoffee.CustomersApp.View.ViewModel
{
    public class CustomersViewModel : ViewModelBase
    {
        private readonly ICustomerDataProvider _customerDataProvider;
        private CustomerItemViewModel? _selectedCustomer;
        private NavigationSide _navigationSide;

        public CustomersViewModel(ICustomerDataProvider customerDataProvider)
        {

            _customerDataProvider = customerDataProvider;
        }
        public ObservableCollection<CustomerItemViewModel> Customers { get; } = new();

        public CustomerItemViewModel? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged();
            }
        }


        public async Task LoadAsync()
        {
            if (Customers.Any())
            {
                return;
            }

            var customers = await _customerDataProvider.GetAllAsync();

            if (customers is not null)
            {

                foreach (var customer in customers)
                {

                    Customers.Add(new CustomerItemViewModel(customer));
                }
            }
        }

        internal void Add()
        {
            var customer = new Customer { FirstName = "New" };
            var viewModel = new CustomerItemViewModel(customer);
            Customers.Add(viewModel);
            SelectedCustomer = viewModel;
        }

        public NavigationSide NavigationSide
        { get => _navigationSide;
          private set
          {
           _navigationSide = value;
           RaisePropertyChanged();
          }
        }
        internal void ModeNavigation()
        {
            NavigationSide = NavigationSide == NavigationSide.Left
                ?NavigationSide.Right
                :NavigationSide.Left;
        }
    }

    public enum NavigationSide
    {
        Left,
        Right
    }
}