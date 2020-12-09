using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OsCoreApplication.Services;

namespace OsCoreApplication.App_Start
{
    public class ActionFilterConfig : ActionFilterAttribute
    {
        private ITransactionProvider _transactionProvider;

        public ActionFilterConfig(ITransactionProvider transactionProvider)
        {
            _transactionProvider = transactionProvider;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _transactionProvider.Begin(System.Data.IsolationLevel.ReadCommitted);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                if (filterContext.Exception == null)
                {
                    _transactionProvider.Commit();
                }
                else
                {
                    _transactionProvider.RollBack();
                }
            }
            catch (Exception ex)
            {
                _transactionProvider.RollBack();
                throw ex;
            }
        }
    }
}