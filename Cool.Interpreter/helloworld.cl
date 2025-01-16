class Main inherits IO {
    test() : SELF_TYPE {
        {
            out_string("Hello from test!");
        }
    };
    main() : SELF_TYPE {
        {
            test();

            x <- not true;
            x <- false;

            out_string(5 + 8);
            i <- 1;
            out_string(i);
            
            name <- "John 'Nickname' Whatever";
            out_string("Hello, World!");
            out_string("name:"+name);
            
            j <- i + 999;

            z <- i < j;

            out_string(j);
            out_string("Bye, World!");
        }
    };
};