namespace Impart.Scripting
{
    public struct KeyEventArgs
    {
        public Type type;
        public KeyEventArgs(Type type)
        {
            this.type = type;
        }
        public enum Type
        {
            a,
            b,
            c,
            d,
            e,
            f,
            g,
            h,
            i,
            j,
            k,
            l,
            m,
            n,
            o,
            p,
            q,
            r,
            s,
            t,
            u,
            v,
            w,
            x,
            y,
            z,
            tab,
            enter,
            shift,
            backspace,
            ctrl,
            alt,
            space,
            n1,
            n2,
            n3,
            n4,
            n5,
            n6,
            n7,
            n8,
            n9,
            n0
        }
    }
}