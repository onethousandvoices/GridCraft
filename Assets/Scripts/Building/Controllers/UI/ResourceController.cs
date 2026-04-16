using System;
using GridCraft.Construction.Runtime;
using VContainer;
using VContainer.Unity;

namespace GridCraft.Construction
{
    public sealed class ResourceController : IStartable, IDisposable
    {
        [Inject] private ResourceBarView _resourceBarView;
        [Inject] private WalletService _walletService;

        public void Start()
        {
            _walletService.Changed += Refresh;
            Refresh();
        }

        private void Refresh() => _resourceBarView.ShowBalance(_walletService.Balance);

        public void Dispose() => _walletService.Changed -= Refresh;
    }
}