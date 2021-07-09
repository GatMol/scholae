/*
  Warnings:

  - You are about to drop the column `Path` on the `Libro` table. All the data in the column will be lost.
  - Added the required column `Immagine` to the `Libro` table without a default value. This is not possible if the table is not empty.

*/
-- AlterTable
ALTER TABLE "Libro" DROP COLUMN "Path",
ADD COLUMN     "Immagine" VARCHAR NOT NULL;
