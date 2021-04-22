-- CreateTable
CREATE TABLE "Utente" (
    "Id" BIGSERIAL NOT NULL,
    "Email" VARCHAR NOT NULL,
    "Nome" VARCHAR NOT NULL,
    "Cognome" VARCHAR NOT NULL,
    "Password" VARCHAR NOT NULL,
    "Telefono" VARCHAR NOT NULL,
    "Citta" VARCHAR,
    "Nazionalita" VARCHAR NOT NULL,

    PRIMARY KEY ("Id")
);

-- CreateTable
CREATE TABLE "Materia" (
    "Nome" VARCHAR NOT NULL,

    PRIMARY KEY ("Nome")
);

-- CreateTable
CREATE TABLE "Libro" (
    "Id" BIGSERIAL NOT NULL,
    "ISBN" VARCHAR,
    "Nome" VARCHAR NOT NULL,
    "Autore" VARCHAR NOT NULL,
    "Edizione" VARCHAR NOT NULL,
    "Editore" VARCHAR NOT NULL,
    "Prezzo" MONEY NOT NULL,
    "Materia" VARCHAR NOT NULL,
    "Utente" BIGINT NOT NULL,

    PRIMARY KEY ("Id")
);

-- CreateTable
CREATE TABLE "LibroSalvato" (
    "Libro" BIGINT NOT NULL,
    "Utente" BIGINT NOT NULL,

    PRIMARY KEY ("Libro","Utente")
);

-- CreateIndex
CREATE UNIQUE INDEX "Utente.Email_unique" ON "Utente"("Email");

-- AddForeignKey
ALTER TABLE "Libro" ADD FOREIGN KEY ("Materia") REFERENCES "Materia"("Nome") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "Libro" ADD FOREIGN KEY ("Utente") REFERENCES "Utente"("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "LibroSalvato" ADD FOREIGN KEY ("Libro") REFERENCES "Libro"("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "LibroSalvato" ADD FOREIGN KEY ("Utente") REFERENCES "Utente"("Id") ON DELETE CASCADE ON UPDATE CASCADE;
