using Cibertec.Models;
using log4net;

namespace Cibertec.Business.Rules
{
    public abstract class BaseProducto : IRule
    {
        private readonly ILog log = LogManager.GetLogger(typeof(BaseProducto));
        public abstract bool IsApplicable(Producto producto);
       //Patron Templeate
        public void Process(Producto producto)
        {
        log.Info("Inicio - Process Common para todas las estrategias");
        CustomProcess(producto);
        log.Info("Fin - Process Common para todas las estrategias");
        }

        internal abstract void CustomProcess(Producto producto);
    }
}
