using System;

namespace MultiPlyUpdates {
    public class Multiplayer {
        private readonly Filer filer;
        public Multiplayer()
        {
            filer = new Filer();
        }

        public void Run()
        {
            var nodes = filer.GetNodes();
            MultyplayFiles(nodes);
        }

        private void MultyplayFiles(string[] nodes)
        {
            filer.Preparation();
            foreach (var node in nodes)
            {
                filer.CreateCopyUpdate(node);
            }
            filer.Finaller();
        }
    }
}