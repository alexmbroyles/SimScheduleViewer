using System.Deployment.Application;

namespace SimScheduleViewer.code
{
    public class version
    {

        public static string GetVersion()
        {
            string version = null;
            try
            {
                //// get deployment version
                version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (InvalidDeploymentException)
            {
                //// you cannot read publish version when app isn't installed 
                //// (e.g. during debug)
                version = "running in debug";
            }
            return version;
        }

    }
}
