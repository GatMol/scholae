-- CreateTable
CREATE TABLE "Libro" (
    "Id" BIGSERIAL NOT NULL,
    "ISBN" VARCHAR,
    "Nome" VARCHAR NOT NULL,
    "Autore" VARCHAR NOT NULL,
    "Edizione" VARCHAR NOT NULL,
    "Editore" VARCHAR NOT NULL,
    "Prezzo" MONEY NOT NULL,
    "Immagine" VARCHAR,
    "Materia_id" VARCHAR NOT NULL,
    "Utente_id" BIGINT NOT NULL,

    PRIMARY KEY ("Id")
);

-- CreateTable
CREATE TABLE "LibroSalvato" (
    "Libro_id" BIGINT NOT NULL,
    "Utente_id" BIGINT NOT NULL,

    PRIMARY KEY ("Libro_id","Utente_id")
);

-- CreateTable
CREATE TABLE "Materia" (
    "Nome" VARCHAR NOT NULL,

    PRIMARY KEY ("Nome")
);

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

-- CreateIndex
CREATE UNIQUE INDEX "Utente.Email_unique" ON "Utente"("Email");

-- AddForeignKey
ALTER TABLE "Libro" ADD FOREIGN KEY ("Materia_id") REFERENCES "Materia"("Nome") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "Libro" ADD FOREIGN KEY ("Utente_id") REFERENCES "Utente"("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "LibroSalvato" ADD FOREIGN KEY ("Libro_id") REFERENCES "Libro"("Id") ON DELETE CASCADE ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "LibroSalvato" ADD FOREIGN KEY ("Utente_id") REFERENCES "Utente"("Id") ON DELETE CASCADE ON UPDATE CASCADE;
