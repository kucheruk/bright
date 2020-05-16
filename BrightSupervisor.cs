using Akka.Actor;

namespace bright
{
    public class BrightSupervisor : ReceiveActor
    {
        protected override void PreStart()
        {
            Context.ChildWithBackoffSupervision<GitlabProjectsScannerActor>();
            base.PreStart();
        }
    }
}