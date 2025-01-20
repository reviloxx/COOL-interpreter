class Main inherits IO {
    is_prime(n : Int, math : Math) : Bool { 
        {
            if n < 2 then
                {
                    false;
                }
            else
               {
                   i <- 2;
                   result <- true;
                   
                   out_string("Testing n: ");
                   out_int(n);
                   
                   while i < n loop
                       {
                           out_string("Testing divisor i: ");
                           out_int(i);
                           out_string(" modulo result: ");
                           out_int(math.modulo(n, i));
                           
                           if math.modulo(n, i) = 0 then
                               {
                                   out_string(" Found divisor! Setting result to false");
                                   result <- false;
                               }
                           else 
                               {
                                   result;
                               };
                           i <- i + 1;
                       }
                   pool;
                   
                   out_string(" Final result for n: ");
                   out_int(n);
                   out_string(" is: ");
                   if result then out_string("true") else out_string("false");
                   
                   result;
               }
            
        }
    };
    main() : SELF_TYPE {
        {
            math <- new Math;
            i <- 2;
            while i < 20 loop 
                {
                    if is_prime(i, math) then
                        {
                            out_int(i);
                        }
                    else
                        {
                            true;
                        };
                    i <- i + 1;
                }
            pool;
            
            out_string("Bye, World!");
            self;
        }
    };
};

class Math {
    modulo(a : Int, b : Int) : Int {
        {
            if b = 0 then
                {
                    out_string("Error: Division by zero in modulo operation.");
                    0;
                }
            else
                (a - (a / b) * b)
            
        }
    };
};