VAR situacionBath = -1
Es la bañera.
-> Inicio

=== Inicio ===
{situacionBath == -1: -> noTapon}
{situacionBath == 0: -> tienesTapon}
{situacionBath >= 1: -> taponPuesto}
->END

=== noTapon ===
¿Quieres abrir el grifo?
***Si
    ->echarAgua
***No
->END

=== echarAgua ===
    ¿A que temperatura?
    ***Fría
        Abres el grifo y dejas el agua correr.
        Para no desperdiciar agua, cierras el grifo.
    ->END
    ***Templada
        Abres el grifo y dejas el agua correr.
        Para no desperdiciar agua, cierras el grifo.
    ->END
    ***Caliente
        Abres el grifo y dejas el agua correr.
        Para no desperdiciar agua, cierras el grifo.
    ->END

=== tienesTapon ===
    ¿Quieres poner el tapón?
    ***Poner
        ->ponerTapon
    ***No poner
-> END


=== taponPuesto ===
    ¿Que quieres hacer?
    ***Quitar tapón
        ->quitarTapon
    ***Abrir grifo
        ->llenarBanera
->END

=== ponerTapon ===
    Colocas el tapón en la bañera.
    ~situacionBath=1
->END

=== quitarTapon ===
    Quitas el tapon y lo guardas.
    ~situacionBath=0
->END

=== llenarBanera ===
    ¿A que temperatura?
    ***Fría
        Abres el grifo y dejas el agua correr.
        La bañera esta llena de agua fría.
        ~situacionBath=2
    ->END
    ***Templada
        Abres el grifo y dejas el agua correr.
        La bañera esta llena de agua templada.
        ~situacionBath=3
    ->END
    ***Caliente
        Abres el grifo y dejas el agua correr.
        La bañera esta llena de agua caliente.
        ~situacionBath=4
    ->END