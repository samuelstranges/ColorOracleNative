
namespace Color_Test_WPF_App_NET_Framework
{

    /// <summary>
    /// Inferface for Different kind of Simulator
    /// </summary>
    interface Filter
    {

        /// <summary>
        /// Simulate diffenrent kind of color blindness vision
        /// </summary>
        /// <param name="inData">pixels data to be processed</param>
        /// <returns>processed pixels data</returns>
        int[] filter(int[] inData);
    }
}
