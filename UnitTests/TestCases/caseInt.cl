﻿class Main inherits IO {
    main() : SELF_TYPE {
        {
            input <- 42;
            case input of
                i : Int => out_int(i + 10);
                s : String => out_string(s);
            esac;
        }
    };
};