using System.Collections.ObjectModel;
using WiredBrainCoffee.CustomersApp.Command;
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
            AddCommand = new DelegateCommand(Add);
            MoveNavigationCommand = new DelegateCommand(MoveNavigation);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
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

        public ObservableCollection<CustomerItemViewModel> Customers { get; } = new();
        public CustomerItemViewModel? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                RaisePropertyChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        public NavigationSide NavigationSide
        {
            get => _navigationSide;
            private set
            {
                _navigationSide = value;
                RaisePropertyChanged();
            }
        }
        public DelegateCommand AddCommand { get; }
        private void Add(object? parameter)
        {
            var customer = new Customer { FirstName = "New" };
            var viewModel = new CustomerItemViewModel(customer);
            Customers.Add(viewModel);
            SelectedCustomer = viewModel;
        }

        public DelegateCommand MoveNavigationCommand { get; }

        public DelegateCommand DeleteCommand { get; }

        private void MoveNavigation(object? paramenter)
        {
            NavigationSide = NavigationSide == NavigationSide.Left
                ?NavigationSide.Right
                :NavigationSide.Left;
        }
        private void Delete(object? parameter)
        {
            if(SelectedCustomer is not null)
            {
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
            }
        }

        private bool CanDelete(object? parameter) => SelectedCustomer is not null;
     
    }

    public enum NavigationSide
    {
        Left,
        Right
    }
}