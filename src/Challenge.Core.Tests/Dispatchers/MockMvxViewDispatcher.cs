using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Base;
using MvvmCross.ViewModels;
using MvvmCross.Views;

namespace Challenge.Core.Tests.Dispatchers
{
    public class MockMvxViewDispatcher : MvxMainThreadDispatcher, IMvxViewDispatcher
    {
        public List<MvxViewModelRequest> NavigateRequests = new List<MvxViewModelRequest>();
        public readonly List<MvxPresentationHint> Hints = new List<MvxPresentationHint>();
 
        async Task<bool> IMvxViewDispatcher.ChangePresentation(MvxPresentationHint hint)
        {
            Hints.Add(hint);
            await Task.Delay(0);
            return true;
        }

        async Task<bool> IMvxViewDispatcher.ShowViewModel(MvxViewModelRequest request)
        {
            NavigateRequests.Add(request);
            await Task.Delay(0);
            return true;
        }

        public override bool RequestMainThreadAction(Action action, bool maskExceptions = true)
        {
            action();
            return true;
        }

        public async Task ExecuteOnMainThreadAsync(Action action, bool maskExceptions = true)
        {
            action();
            await Task.Delay(0);
        }

        public async Task ExecuteOnMainThreadAsync(Func<Task> action, bool maskExceptions = true)
        {
            await action();
            await Task.Delay(0);
        }

        public override bool IsOnMainThread { get; }
    }
}