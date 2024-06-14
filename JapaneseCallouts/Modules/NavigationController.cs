namespace JapaneseCallouts.Modules;

internal class LIFOStack<T> : LinkedList<T>
{
    internal T Pop()
    {
        T first = this.First<T>();
        RemoveFirst();
        return first;
    }

    internal void Push(T item)
    {
        AddFirst(item);
    }

    internal T Peek()
    {
        return this.First<T>();
    }

    // Remove(T object) implemented in LinkedList
}

internal class NavigationController
{
    protected internal struct NavigationEntry
    {
        internal GwenForm form;
        internal bool isRoot;
        internal NavigationEntry(GwenForm form, bool isRoot = false)
        {
            this.form = form;
            this.isRoot = isRoot;
        }
    }

    internal delegate void FormChangeEventArgs(object sender, NavigationEntry form);
    internal event FormChangeEventArgs OnFormAdded;
    internal event FormChangeEventArgs OnFormRemoved;

    private readonly LIFOStack<NavigationEntry> Stack = new();

    internal int Size
    {
        get
        {
            return Stack.Count;
        }
    }

    internal GwenForm Head
    {
        get
        {
            return Stack.Count > 0 ? Stack.Peek().form : null;
        }
    }

    internal List<GwenForm> Tail
    {
        get
        {
            var stack = Stack;
            return stack.Count > 1 ? Stack.Skip(1).Select(x => x.form).ToList() : null;
        }
    }

    internal bool IsEmpty
    {
        get { return Size > 0; }
    }

    internal bool HasOpenForms
    {
        get
        {
            lock (Stack)
            {
                return Stack.Any(x => x.form != null && x.form.IsOpen());
            }
        }
    }

    internal NavigationEntry Push(GwenForm form, bool notify = true)
    {
        var entry = new NavigationEntry(form);
        Stack.Push(entry);
        if (notify && OnFormAdded != null)
            OnFormAdded(this, entry);
        return entry;
    }

    internal bool RemoveEntry(NavigationEntry entry, bool notify = true)
    {
        try
        {
            if (Stack.Contains(entry))
            {
                Stack.Remove(entry);
                if (notify && OnFormRemoved != null)
                    OnFormRemoved(this, entry);
                return true;
            }
        }
        catch (Exception e)
        {
            Logger.Error(e.ToString());
        }
        return false;
    }

    internal void Replace(GwenForm form)
    {
        Clear();
        var entry = new NavigationEntry(form, true);
        Stack.Push(entry);
        OnFormAdded?.Invoke(this, entry);
    }

    internal void Clear()
    {
        if (OnFormRemoved != null)
        {
            foreach (var entry in Stack.Where(x => x.form != null))
            {
                OnFormRemoved(this, entry);
            }
        }
        Stack.Clear();
    }
}