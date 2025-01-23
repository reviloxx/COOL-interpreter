class Main inherits IO {
    main() : SELF_TYPE {
        {
            i <- 0;

            while i < 5 loop
            {
                out_int(i);
                i <- i + 1;
            }
        }
    };
};