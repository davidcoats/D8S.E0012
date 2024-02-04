using System;


namespace D8S.E0012
{
    public class BlazorRenderingOperator : IBlazorRenderingOperator
    {
        #region Infrastructure

        public static IBlazorRenderingOperator Instance { get; } = new BlazorRenderingOperator();


        private BlazorRenderingOperator()
        {
        }

        #endregion
    }
}
