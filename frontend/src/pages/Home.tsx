import { useEffect, useState } from "react";
import { PropertyService } from "../services/property.service";
import { IPropertyDto } from "../dtos/property/property-dto";
import { IPropertyFiltersDto } from "../dtos/property/propert-filters-dto";
import PropertySearchForm from "../components/property-search-form";
import PropertyCard from "../components/property-card";

export default function Home() {
	const [filters, setFilters] = useState<IPropertyFiltersDto>({
		title: null,
		bathrooms: null,
		bedrooms: null,
		carspots: null,
		listingType: null,
		maxPrice: null,
		minPrice: null
	});

	const [properties, setProperties] = useState<IPropertyDto[]>([]);
	const [loading, setLoading] = useState(false);
	const [error, setError] = useState<string | null>(null);

	const fetchProperties = async (filters: IPropertyFiltersDto) => {
		setLoading(true);
		setError(null);
		try {
			const response = await PropertyService.GetProperties(filters);
			if (response.success && response.response) {
				setProperties(response.response);
			}
		} catch (err) {
			setError(err instanceof Error ? err.message : 'Failed to fetch properties');
		} finally {
			setLoading(false);
		}
	};

	useEffect(() => {
		fetchProperties(filters);
	}, [filters]);

	const searchProperties = (filters: IPropertyFiltersDto) => {
		setFilters(filters);
	}

	const reloadProperties = () => {
		fetchProperties(filters);
	}

	return <>
		<PropertySearchForm defaultFilters={filters} onSeach={searchProperties} />

		{loading && <div>Loading...</div>}
		{error && <div className="error">{error}</div>}

		<div className="row property-list pt-4">
			{
				properties.length ?
					properties.map(property =>
						(<PropertyCard key={property.id} property={property} reload={reloadProperties} />)
					) : (
						<div className="col-12">
							<span>No record found.</span>
						</div>)
			}
		</div>
	</>;
}
