using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.Core.Commands
{
  public interface IApplicationCommands
  {
    CompositeCommand SaveAllCommand { get; }
  }
  class ApplicationCommands
  {
    public CompositeCommand SaveAllCommand { get; } = new CompositeCommand();
  }
}
