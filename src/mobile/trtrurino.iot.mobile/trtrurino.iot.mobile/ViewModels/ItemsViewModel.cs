using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using trtrurino.iot.mobile.Services;
using Xamarin.Forms;
using Device = trtrurino.iot.mobile.Models.Device;

namespace trtrurino.iot.mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private readonly IDeviceService _deviceService;

        public ObservableCollection<Device> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public Command SetStatus { get; set; }

        public ItemsViewModel(IDeviceService deviceService)
        {
            _deviceService = deviceService;
            Title = "Devices";
            Items = new ObservableCollection<Device>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            SetStatus = new Command(async (d) => await ExecuteSetStatusCommand((Device)d));
        }

        private async Task ExecuteSetStatusCommand(Device device)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                await _deviceService.SetStatus(device.Id, device.Status);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            await ExecuteLoadItemsCommand();
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await _deviceService.GetDevices();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}