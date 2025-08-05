**UrlShortenerMinimalAPI**

A simple URL shortener built with .NET 9 Minimal API.

**Features**

POST /shorten – Send a long URL and get a short version back.
GET /{code} – Redirects to the original URL using the short code.
In-memory storage using Dictionary<string, string>.
Auto-generated short codes using Base32 characters.
Swagger support for easy testing.

**How to Run**
Clone the repo:
git clone https://github.com/safakca/UrlShortenerMinimalAPI.git
cd UrlShortenerMinimalAPI

**Run the app:**
dotnet run

**Open Swagger:**
http://localhost:5000/swagger


**Example Usage**
**Request:**

POST /shorten
Content-Type: application/json

{
  "longUrl": "https://example.com"
}

**Response:**
"http://localhost:5000/ABC123"

**Redirect:**
GET /ABC123 → 302 Redirect to original URL

