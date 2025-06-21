using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Grafos
{
    internal class FiladePrioridades<TItem>
        where TItem : IPriorizable
    {
        public LinkedList<TItem> Fila { get; } = new LinkedList<TItem>();

        public int Count()
        {
            return Fila.Count;
        }

        public TItem Dequeue()
        {
            if (Fila.Any())
            {
                var itemTobeRemoved = Fila.First.Value;
                Fila.RemoveFirst();
                return itemTobeRemoved;
            }

            return default(TItem);
        }

        public void Enqueue(TItem entry)
        {
            var value = new LinkedListNode<TItem>(entry);
            if (Fila.First == null)
            {
                Fila.AddFirst(value);
            }
            else
            {
                var ptr = Fila.First;
                while (ptr.Next != null && ptr.Value.Priority < entry.Priority)
                {
                    ptr = ptr.Next;
                }

                if (ptr.Value.Priority <= entry.Priority)
                {
                    Fila.AddAfter(ptr, value);
                }
                else
                {
                    Fila.AddBefore(ptr, value);
                }
            }
        }

        public bool IsEmpty()
        {
            return Fila.Count == 0;
        }
    }
}
