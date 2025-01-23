class Main inherits IO {
    main() : SELF_TYPE {
        {
            value <- 5;
            case value of
                1 => out_string("Value is one.");
                2 => out_string("Value is two.");
                3 => out_string("Value is three.");
                4 => out_string("Value is four.");
                5 => out_string("Value is five.");
                6 => out_string("Value is six.");
                7 => out_string("Value is seven.");
                8 => out_string("Value is eight.");
                9 => out_string("Value is nine.");
                10 => out_string("Value is ten.");
                _ => out_string("Value is something else.");
                esac;
            self;
        }
    };
};
