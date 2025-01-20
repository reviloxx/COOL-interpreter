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
            out_string(z);
            
            
            
            -- Instantiate the Math class and use the modulo method
            math <- new Math;
            result <- math.modulo(15, 4);  -- Example usage of modulo
            out_string("15 modulo 4 = ");
            out_int(result);


            
            out_string("Bye, World!");
            
            
        }
    };
};

class Math {
    modulo(a : Int, b : Int) : SELF_TYPE {
        {
            if b = 0 then
                {
                    out_string("Error: Division by zero in modulo operation.\n");
                    0;  -- Return 0 as a fallback
                }
            else
                a - (a / b) * b;  -- Standard modulo implementation
        }
    };
};