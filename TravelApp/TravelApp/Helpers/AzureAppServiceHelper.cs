#define OFFLNE_SYNC_ENABLED

using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Helpers
{
    public class AzureAppServiceHelper
    {
        public static async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await App.MobileService.SyncContext.PushAsync();
                System.Diagnostics.Debug.WriteLine("am facut push: ");
                await App.postsTable.PullAsync("posts", App.postsTable.CreateQuery());
                System.Diagnostics.Debug.WriteLine("am facut pull: ");
            }
            catch(MobileServicePushFailedException mspfe)
            {
                if (mspfe.PushResult != null)
                    syncErrors = mspfe.PushResult.Errors;
                System.Diagnostics.Debug.WriteLine("Exception in SyncPush EXC: " + mspfe.Message, "ERROR");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception in SyncPush EXC: " + ex.Message, "ERROR");

            }

            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        await error.CancelAndDiscardItemAsync();
                    }
                }
            }
        }
    }
}
