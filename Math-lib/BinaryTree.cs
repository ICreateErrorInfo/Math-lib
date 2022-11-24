namespace Math_lib {
    public class BinaryTreeNode {
        private int _value;
        private BinaryTreeNode _left;
        private BinaryTreeNode _right;

        public BinaryTreeNode(int value) {
            _value = value;
            _left = null;
            _right = null;
        }

        public int GetValue() {
            return _value;
        }

        public void AddNode(int value) {
            if(value < _value) {
                if(_left== null) {
                    _left = new BinaryTreeNode(value);
                } else {
                    _left.AddNode(value);
                }
            } else {
                if (_right == null) {
                    _right = new BinaryTreeNode(value);
                } else {
                    _right.AddNode(value);
                }
            }
        }

        public void AddNodes(BinaryTreeNode left, BinaryTreeNode right) {
            _left = left;
            _right = right;
        }
    }
}
