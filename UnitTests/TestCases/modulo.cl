class Main inherits IO {
    main() : SELF_TYPE {
        {
            math <- new Math;            
            out_string("is math.modulo(4, 2) = 0?");
            out_stringln(math.modulo(4, 2) = 0);

            out_string("is math.modulo(4, 3) = 0?");
            out_stringln(math.modulo(4, 3) = 0);
        }
    };
};

class Math {
    modulo(a : Int, b : Int) : Int {
        {
            if b = 0 then
            {
                out_stringln("Error: Division by zero in modulo operation.");
                0;
            }
            else
            {
                (a - (a / b) * b)
            }                        
        }
    };
};