import { SubmitHandler, useForm } from "react-hook-form";
import { IPropertyFiltersDto } from "../dtos/property/propert-filters-dto";

type Props = {
	defaultFilters: IPropertyFiltersDto,
	onSeach: (filters: IPropertyFiltersDto) => void
};

export default function PropertySearchForm({ defaultFilters, onSeach }: Props) {
	const {
		register,
		handleSubmit,
		formState: { errors, isSubmitting },
	} = useForm<IPropertyFiltersDto>({
		mode: 'onSubmit',
		defaultValues: {
			title: defaultFilters.title,
			bathrooms: defaultFilters.bathrooms,
			bedrooms: defaultFilters.bedrooms,
			carspots: defaultFilters.carspots,
			listingType: defaultFilters.listingType,
			maxPrice: defaultFilters.maxPrice,
			minPrice: defaultFilters.minPrice
		},
	});

	const onSubmit: SubmitHandler<IPropertyFiltersDto> = (filters: IPropertyFiltersDto) => {
		onSeach(filters);
	};

	return (
		<form className="py-4" onSubmit={handleSubmit(onSubmit)}>
			<div className="row">
				<div className="col-3 d-flex justify-content-between">
					<label>Type</label>
					<div>
						<div className="form-check form-check-inline">
							<input className="form-check-input" type="radio" id="radioDefault1" value="0" {...register('listingType')} />
							<label className="form-check-label" htmlFor="radioDefault1">
								Rent
							</label>
						</div>
						<div className="form-check form-check-inline">
							<input className="form-check-input" type="radio" id="radioDefault2" value="1" {...register('listingType')} />
							<label className="form-check-label" htmlFor="radioDefault2">
								Sale
							</label>
						</div>
					</div>
				</div>
				<div className="col-3">
					<label>Title</label>
					<input type="text"
						className="form-control"
						placeholder="Title"
						{...register('title')}
					/>
				</div>
				<div className="col-3">
					<label>Min Price</label>
					<input type="number"
						className="form-control"
						placeholder="Min Price"
						{...register('minPrice')}
					/>
				</div>
				<div className="col-3">
					<label>Max Price</label>
					<input type="number"
						className="form-control"
						placeholder="Max Price"
						{...register('maxPrice')}
					/>
				</div>
				<div className="col-3">
					<label>Bed Rooms</label>
					<input type="number"
						className="form-control"
						placeholder="BedRomms"
						{...register('bedrooms')}
					/>
				</div>
				<div className="col-3">
					<label>BathRooms</label>
					<input type="number"
						className="form-control"
						placeholder="BathRooms"
						{...register('bathrooms')}
					/>
				</div>
				<div className="col-3">
					<label>Car Spots</label>
					<input type="number"
						className="form-control"
						placeholder="Car Spots"
						{...register('carspots')}
					/>
				</div>

				<div className="col-3 d-flex align-items-center pt-4">
					<button type="submit" className="btn btn-primary btn-block w-100" disabled={isSubmitting}>
						{isSubmitting ? 'Submitting...' : 'Search'}
					</button>
				</div>

			</div>
		</form>
	)
}
