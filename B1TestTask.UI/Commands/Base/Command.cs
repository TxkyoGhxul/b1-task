using System.Windows.Input;

namespace B1TestTask.UI.Commands.Base;
internal abstract class Command : ICommand
{
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public virtual bool CanExecute(object? parameter) => true;
    public abstract void Execute(object? parameter);

    public abstract class WithParams<T> : Command where T : class
    {
        public override void Execute(object? parameter)
        {
            if (parameter is not T @params || !ValidateParams(@params))
            {
                return;
            }

            Execute(@params);
        }

        public override bool CanExecute(object? parameter) =>
            parameter is T @params && ValidateParams(@params);

        protected abstract bool ValidateParams(T @params);
        protected abstract void Execute(T @params);
    }
}
