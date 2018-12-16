
using System;

namespace KingsgateMediaPlayerFeedCacher.Exceptions {

    public class UnhandedTrigger : Exception {

        public UnhandedTrigger() {

        }

        public UnhandedTrigger(String message) : base(message) {
            
        }
    }

}